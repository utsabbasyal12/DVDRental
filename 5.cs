// 5
shopList = _context.Shop.ToList();
memberList = _context.Member;
dvdCopyList = _context.DVDCopy;
dvdTitleList = _context.DVDTitle;
studio = _context.Studio;
producer = _context.Producer;
loan = _context.Loan;

userDetails = "HIVE MAGICK FUCKERY";
userShopID = userDetails.ShopID;
userShop = shopList.Where(shop => shop.id == userShopID);

dvdCopiesFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			);

joinCopyTitleLoanMember = dvdCopiesFromShop.Join(dvdTitleList,
			copy => copy.DVDNumber,
			title => title.DVDNumber,
			(copy, title) => new{
				copy, title
			}
			).Join(loan,
			copyTitle => copyTitle.copyNumber,
			loan => loan.loanNumber,
			(copyTitle, loan) => new{
				copy = copyTitle.copy,
				title = copyTitle.title,
				loan.OrderBy(x=> x.dateOut).LastOrDefaut()
			}
			).Join(memberList,
			copyTiteLoan => copyTitleLoan.loan.memberNumber,
			member => member.memberNumber,
			(copyTiteLoan, member) => new{
				copy = copyTiteLoan.copy,
				title = copyTiteLoan.title,
				loan = copyTiteLoan.loan,
				member
			}
			);

