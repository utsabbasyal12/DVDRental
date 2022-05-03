// 1
var shopList = _context.Shop.ToList();
var dvdCopyList = _context.DVDCopy;
var userDetails = "HIVE MAGICK FUCKERY";
var userShopID = userDetails.ShopID;
var userShop = shopList.Where(shop => shop.id == userShopID);

var dvdCopiesFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			);

var dvdTitle = _context.DVDTitle;

var dvdTitles = dvdCopiesFromShop.Join(dvdTitle,
			dvdCopy => dvdCopy.CoypNo,
			dvdTitle => title.CopyNo,
			(dvdCopy, dvdTitle) => dvdTitle
			).Distinct();

var requestActorNumber = request.actorNumber;
var ActorList = _context.Actor;

var dvdTitlesWithActor = dvdTitles.Join(CastMember,
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
			);

var dvdTitlesWithSelectedActor = dvdTitlesWithActor.Where(title => title.actorNumber == requestActorNumber);

