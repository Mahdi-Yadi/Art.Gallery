import BigCardItem from "../common/BigCardItem";

const SportsUpdate = ({ Title, Description }) => {
  const CardItemsData = [
    {
      Title: "سه ترکیب احتمالی انگلیس مقابل ایران",
      CardImage: "src/assets/image/blog/16by9/06.jpg",
      Author: "مهدی کریمی",
      AuthorAvatar: "src/assets/image/avatar/03.jpg",
      Date: "17 تیر، 1400",
      BadgeText: "ورزش",
      BadgeClassName: "badge text-bg-danger mb-2",
      BadgeLinkUrl: "#",
      ReadingTime: 6,
    },
    {
      Title: "بسته ۲۰۲۲ ایرانسل ویژه جام جهانی ارائه شد",
      CardImage: "src/assets/image/blog/16by9/01.jpg",
      Author: "سمانه محمدی",
      AuthorAvatar: "src/assets/image/avatar/09.jpg",
      Date: "29 مرداد، 1400",
      BadgeText: "ورزش",
      BadgeClassName: "badge text-bg-warning mb-2",
      BadgeLinkUrl: "#",
      ReadingTime: 2,
    },
  ];

  return (
    <section>
      <div className="container">
        {/* -----| Section Header |----- */}
        <div className="row">
          <div className="col-12 mb-4">
            <h2 className="m-0">
              <i className="bi bi-controller me-2"></i>
              {Title}
            </h2>
            <p className="m-0">{Description}</p>
          </div>
        </div>
        {/* -----| End of Section Header |----- */}

        {/* -----| Main Content |----- */}
        <div className="row">
          <div className="col-md-6 mb-4 mb-md-0">
            {/* --- Blog Card --- */}
            <BigCardItem
              Title={CardItemsData[0].Title}
              CardImage={CardItemsData[0].CardImage}
              Author={CardItemsData[0].Author}
              AuthorAvatar={CardItemsData[0].AuthorAvatar}
              Date={CardItemsData[0].Date}
              BadgeText={CardItemsData[0].BadgeText}
              BadgeClassName={CardItemsData[0].BadgeClassName}
              BadgeLinkUrl={CardItemsData[0].BadgeLinkUrl}
              ReadingTime={CardItemsData[0].ReadingTime}
            />
            {/* --- End of Blog Card --- */}
          </div>

          <div className="col-md-6">
            {/* --- Blog Card --- */}
            <BigCardItem
              Title={CardItemsData[1].Title}
              CardImage={CardItemsData[1].CardImage}
              Author={CardItemsData[1].Author}
              AuthorAvatar={CardItemsData[1].AuthorAvatar}
              Date={CardItemsData[1].Date}
              BadgeText={CardItemsData[1].BadgeText}
              BadgeClassName={CardItemsData[1].BadgeClassName}
              BadgeLinkUrl={CardItemsData[1].BadgeLinkUrl}
              ReadingTime={CardItemsData[1].ReadingTime}
            />
            {/* --- End of Blog Card --- */}
          </div>
        </div>
        {/* -----| End of Main Content |----- */}
      </div>
    </section>
  );
};

export default SportsUpdate;
