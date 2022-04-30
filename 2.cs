// 2
shopList = _context.Shop.ToList()
dvdCopyList = _context.DVDCopy.
userDetails = HIVE MAGICK FUCKERY
userShopID = userDetails.ShopID
userShop = shopList.Where(shop => shop.id == userShopID)
Loan = _context.Loan

dvdCopiesFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			)

dvdCopiesOnLoan = dvdCopiesFromShop.Join(Loan,
			dvdCopy => dvdCopy.copyNumber,
			loan => loan.copyNumber,  
			(dvdCopy, loan => new
			  {	
				copyNumber = dvdCopy.copyNumber,
				dateReturned = loan.dateReturned
			  }
			)
			).Where(x => x.dateReturned == null)

dvdCopiesOnShelf = dvdCopiesFromShop.Except(dvdCopiesOnLoan)

dvdTitle = _context.DVDTitle

dvdTitlesOnShelf = dvdCopiesOnShelf.Join(dvdTitle,
			dvdCopy => dvdCopy.CoypNo
			dvdTitle => title.CopyNo,
			(dvdCopy, dvdTitle) => dvdTitle
			).GroupBy(dvdTitle => dvdTitle)

// This should return a dictionary which can be accessed via forEach
// dvdTitlesOnShelf.key gives the title
// dvdTitlesOnShelf.Count() gives the number of title copies on shelf 



