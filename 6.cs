// 6
shopList = _context.Shop.ToList();
memberList = _context.Member;
dvdCopyList = _context.DVDCopy;
dvdTitleList = _context.DVDTitle;
studio = _context.Studio;
producer = _context.Producer;
loan = _context.Loan;
dvdCategory = _context.dvdCategory;

userDetails = "HIVE MAGICK FUCKERY";
userShopID = userDetails.ShopID;
userShop = shopList.Where(shop => shop.id == userShopID);

membersFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			);

selectedMemberNumber = request.memberNumber;

selectedMember = membersFromShop.Where(x=>x.memberNumber == selectedMemberNumber);

int age = 0;
age = DateTime.Now.Subtract(selectedMember.memberDOB).Days / 365;

selectedCopyNumber = request.selectedCopyNumber;
selectedCopy = dvdCopyList.Where(x=>x.copyNumber == selectedCopyNumber);
ageRestricted = selectedCopy.Join(dvdTitleList,
			dvdTitle => dvdTitle.dvdNumber,
			dvdcopy => dvdcopy.dvdNumber,
			(dvdTitle, dvdCopy) => new {
				categoryNumber = dvdTitle.categoryNumber
			}
			).Join(dvdCategory,
			categoryNo => categoryNo,
			dvdCategory => dvdCategory.categoryNymber,
			(categoryNo, dvdCategory) => new{
				dvdCategory.ageRestricted
			}
			);

if (age >=18){
	LoanTheDvd();
}	else if (age<18 && ageRestricted == "false")
{
	LoanTheDVD();	
}   else{
	//ShowAgePugenaMessage
}

lastLoan = loan.Where( x => x.copyNumber).OrderBy(x=>x.DateOut).LastOrDefaut();
Function LoanTheDVD(){
	//get loan details from UI or View and add it in a loan object
	// //DateOut = currentDate
	// //LoanNumber = lastLoan.LoanNumber + 1 
	// //Other Details from request
	//Add it in DB
}

//Calculate Charge
//Charge = standard charge * duration
//duration = DateOut - DateDue