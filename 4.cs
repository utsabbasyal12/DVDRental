// 4
shopList = _context.Shop.ToList();
memberList = _context.Member;
dvdCopyList = _context.DVDCopy;
dvdTitleList = _context.DVDTitle;
studio = _context.Studio;
producer = _context.Producer;

userDetails = "HIVE MAGICK FUCKERY";
userShopID = userDetails.ShopID;
userShop = shopList.Where(shop => shop.id == userShopID);

dvdCopiesFromShop = dvdCopyList.Join(userShop,
			dvdCopy => dvdCopy.ShopID,
			shop => shop.ShopID,
			(dvdCopy, shop) => dvdCopy
			);

dvdTitlesFromShop = dvdTitleList.Join(dvdCopiesFromShop,
			dvdTitle => dvdTitle.DVDNumber,
			dvdCopy => dvdCopy.DVDNumber,
			(dvdTitle, dvdCopy) => dvdTitle
			);

titleStudioProducer = studio.Join(dvdTitlesFromShop,
			studio => studio.studioNumber,
			dvdTitle => dvdTitle.studioNumber,
			(studio, dvdTitle) => new
			{
				// dvdNumber = dvdTitle.dvdNumber,
				// dvdTitle = dvdTitle.Title,
				// dateReleased = dvdTitle.dateReleased,
				// studioNumber=studio.studioNumber,
				// studioName = studio.studioName,
				// producerNumber = dvdTitle.producerNumber,
				dvdTitle,
				studio
			}
			).Join(Producer,
			dvdTitleStudio => dvdTitleStudio.dvdTitle.producerNumber,
			producer => producer.producerNumber,
			(dvdTitleStudio, producer) => new{
				dvdTitle = dvdTitleStudio.dvdTitle,
				studio = dvdTitleStudio.studio,
				producer,
			}
			);