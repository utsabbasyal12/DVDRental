// 6
shopList = _context.Shop.ToList();
memberList = _context.Member;
dvdCopyList = _context.DVDCopy;
dvdTitleList = _context.DVDTitle;
studio = _context.Studio;
producer = _context.Producer;
loan = _context.Loan;
dvdCategory = _context.dvdCategory;
membershipCategory = _context.membershipCategory;

userDetails = "HIVE MAGICK FUCKERY";
userShopID = userDetails.ShopID;
userShop = shopList.Where(shop => shop.id == userShopID);

membersFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			).Join (membershipCategory,
			member => member.membershipCategoryNumber,
			category => category.membershipCategory,
			(member, category) => new{
				member,
				category
			}
			); //returns members from shop along with their membership category

memberLoans = membersFromShop.Join(loan,
			memberCategory => memberCategory.member.memberNumber,
			loan => loan.memberNumber,
			(memberCategory, loan) => new{
				memberNumber => memberCategory.member.memberNumber,
				membershipCategoryDescription => memberCategory.category.description,
				totalLoans => memberCategory.category. membershipCategoryTotalLoans,
				loanNumber => loan.loanNumber,
				dateReturned => loan.dateReturned,
			}
			).where(x => x.dateReturned == null).groupBy(x => x.dateReturned);

