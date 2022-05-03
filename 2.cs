// 2
var shopList = _context.Shop.ToList();
var dvdCopyList = _context.DVDCopy;
var userDetails = "HIVE MAGICK FUCKERY";
var userShopID = userDetails.ShopID;
var userShop = shopList.Where(shop => shop.id == userShopID);
var Loan = _context.Loan;

var dvdCopiesFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			);

var dvdCopiesOnLoan = dvdCopiesFromShop.Join(Loan,
			dvdCopy => dvdCopy.copyNumber,
			loan => loan.copyNumber,  
			(dvdCopy, loan => new
			  {	
				copyNumber = dvdCopy.copyNumber,
				dateReturned = loan.dateReturned
			  }
			)
			).Where(x => x.dateReturned == null);

dvdCopiesOnShelf = dvdCopiesFromShop.Except(dvdCopiesOnLoan);

dvdTitle = _context.DVDTitle;

dvdTitlesOnShelf = dvdCopiesOnShelf.Join(dvdTitle,
			dvdCopy => dvdCopy.CoypNo,
			dvdTitle => title.CopyNo,
			(dvdCopy, dvdTitle) => dvdTitle
			);

requestActorNumber = request.actorNumber;
ActorList = _context.Actor;

dvdTitlesWithActor = dvdTitlesOnShelf.Join(CastMember,
			dvdTitle => dvdTitle.dvdNumber,
			castMember => castMember.dvdNumber,
			(dvdTitle, castMember) => new
			  {
				dvdNumber = dvdTitle.dvdNumber,
				producerNumber = dvdTitle.producerNumber,
				categoryNumber = dvdTitle.categoryNumber,
				studioNumber = dvdTitle.studioNumber,
				dateReleased = dvdTitle.dateReleased,
				standardCharge = dvdTitle.standardCharge,
				penaltyCharge = dvdTitle.penaltyCharge,
				actorNumber = castMember.actorNumber,
				actorName = castMember.actorName
			  }
			).GroupBy(x => x.dvdNumber);

// This should return a dictionary which can be accessed via forEach
// dvdTitlesOnShelf.key gives the title
// dvdTitlesOnShelf.Count() gives the number of title copies on shelf 