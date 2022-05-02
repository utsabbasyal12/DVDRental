// 3
shopList = _context.Shop.ToList();
memberList = _context.Member;
loan = _context.Loan;
dvdCopyList = _context.DVDCopy;
dvdTitleList = _context.DVDTitle;

userDetails = "HIVE MAGICK FUCKERY";
userShopID = userDetails.ShopID;
userShop = shopList.Where(shop => shop.id == userShopID);

membersFromShop = memberList.Join(userShop,
			member => member.ShopID,
			shop => shop.ShopID,
			(member, shop) => member
			);

selectedMember = request.memberNumber;

memberLoans = memberFromShop.Join(loan,
			member => member.memberNumber,
			loan => loan.memberNumber,  
			(member, loan => loan
			)
			).Where(x => x.dateReturned != null) 
			.Where(DateTime.Now() - x.dateReturned <= 31  )
			.Where(x => x.memberNumber == selectedMember);

loanedDVDCopies = dvdCopyList.Join(memberLoans,
			dvdcopy => dvdcopy.memberNumber,
			memberLoan => memberLoan.memberNumber,
			(dvdCopy, memberLoan) => dvdCopy	
			);

loanedDVDTitles = dvdTitleList.Join(loanedDVDCopies,
			dvdTitle => dvdTitle.copyNumber,
			loanedDVDCopy => loanedDVDCopy.copyNumber,
			(dvdTitle, loanedDVDCopy) => dvdTitle
			);

// This should return a list of titles
