// 1
shopList = _context.Shop.ToList();
dvdCopyList = _context.DVDCopy;
userDetails = "HIVE MAGICK FUCKERY";
userShopID = userDetails.ShopID;
userShop = shopList.Where(shop => shop.id == userShopID);

dvdCopiesFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			);

dvdTitle = _context.DVDTitle;

dvdTitles = dvdCopiesFromShop.Join(dvdTitle,
			dvdCopy => dvdCopy.CoypNo,
			dvdTitle => title.CopyNo,
			(dvdCopy, dvdTitle) => dvdTitle
			).Distinct();

requestActorNumber = request.actorNumber;
ActorList = _context.Actor;

dvdTitlesWithActor = dvdTitles.Join(CastMember,
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

dvdTitlesWithSelectedActor = dvdTitlesWithActor.Where(title => title.actorNumber == requestActorNumber);

