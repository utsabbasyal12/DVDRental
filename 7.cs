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

selectedLoanNumber = request.loanNumber;
selectedLoan = loan.Where( x => x.selectedLoanNumber);

UpdateLoan();//Put cutrrentDate in DateReturned 

if (selectedLoan.dateReturned > selectedLoan.dateDue ){
	dvdTitle = selectedLoan.Join(dvdCopyList,
				loan => loan.copyNumber,
				copy => copy.copyNumber,
				(loan, copy) => copy
				).Join(dvdTitleList,
				copy => copy.dvdNumber,
				title => title.dvdNumber,
				(copy, title) => title
				) ;
	penaltyDuration = selectedLoan.dateReturned - selectedLoan.dateDue; //its probably in seconds, if so, change later
	penaltyAmount = dvdTitle.penaltyCharge * penaltyDuration;
}