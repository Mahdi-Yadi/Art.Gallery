import CardItem from "../common/CardItem";

const SponsoredNews = ({ Title }) => {
  const CardItemsData = [
    {
      Title: "حضور ایرانسل در رویداد فناورانه خودروهای آینده",
      CardImage: "src/assets/image/blog/4by3/01.jpg",
      Author: "دنیا اسدی",
      AuthorAvatar: "src/assets/image/avatar/01.jpg",
      Date: "29 مرداد، 1400",
      BadgeText: "مگامنو",
      BadgeClassName: "badge bg-danger bg-opacity-10 text-danger mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "آشنایی با کلید موفقیت نهضت ملی مسکن‌",
      CardImage: "src/assets/image/blog/4by3/02.jpg",
      Author: "علی علیزاده",
      AuthorAvatar: "src/assets/image/avatar/02.jpg",
      Date: "17 تیر، 1400",
      BadgeText: "ورزش",
      BadgeClassName: "badge bg-info bg-opacity-10 text-info mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "رونمایی از طرح بزرگترین تلسکوپ نوری آسیا",
      CardImage: "src/assets/image/blog/4by3/03.jpg",
      Author: "میلاد بابایی",
      AuthorAvatar: "src/assets/image/avatar/03.jpg",
      Date: "7 دی، 1400",
      BadgeText: "اقتصاد",
      BadgeClassName: "badge bg-success bg-opacity-10 text-success mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "کاهش 192 هزار میلیارد تومانی بدهی دولت",
      CardImage: "src/assets/image/blog/4by3/04.jpg",
      Author: "ادمین",
      AuthorAvatar: "src/assets/image/avatar/05.jpg",
      Date: "22 آذر، 1400",
      BadgeText: "تکنولوژی",
      BadgeClassName: "badge bg-warning bg-opacity-15 text-warning mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "طرح مجلس برای گرفتن مالیات از سفته بازها",
      CardImage: "src/assets/image/blog/4by3/05.jpg",
      Author: "میلاد بابایی",
      AuthorAvatar: "src/assets/image/avatar/03.jpg",
      Date: "7 دی، 1400",
      BadgeText: "اجتماعی",
      BadgeClassName: "badge bg-primary bg-opacity-10 text-primary mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "هشدار درباره یک بیماری حاد تنفسی در سرما",
      CardImage: "src/assets/image/blog/4by3/06.jpg",
      Author: "کیمیا آقایی",
      AuthorAvatar: "src/assets/image/avatar/07.jpg",
      Date: "17 تیر، 1400",
      BadgeText: "فرهنگ",
      BadgeClassName: "badge bg-danger bg-opacity-10 text-danger mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
  ];

  return (
    <section className="pt-4">
      <div className="container">
        {/* -----| Section Header |----- */}
        <div className="row">
          <div className="col-md-12">
            <div className="mb-4 d-md-flex justify-content-between align-items-center">
              <h2 className="m-0">
                <i className="bi bi-megaphone me-2"></i>
                {Title}
              </h2>
              <a href="#" className="text-body small">
                <u>مشاهده همه</u>
              </a>
            </div>
          </div>
        </div>
        {/* -----| End of Section Header |----- */}

        {/* -----| Main Content |----- */}
        <div className="row">
          <div className="col-lg-6">
            {/* --- Blog Card Section 1 --- */}
            {CardItemsData.slice(0, 3).map((item, index) => (
              <div key={index}>
                <CardItem
                  Title={item.Title}
                  CardImage={item.CardImage}
                  Author={item.Author}
                  AuthorAvatar={item.AuthorAvatar}
                  Date={item.Date}
                  BadgeText={item.BadgeText}
                  BadgeClassName={item.BadgeClassName}
                  BadgeLinkUrl={item.BadgeLinkUrl}
                  Score={item.Score}
                  Layout={item.Layout}
                />
              </div>
            ))}
            {/* --- End of Blog Card Section 1 --- */}
          </div>

          <div className="col-lg-6">
            {/* --- Blog Card Section 2 --- */}
            {CardItemsData.slice(3, 6).map((item, index) => (
              <div key={index}>
                <CardItem
                  Title={item.Title}
                  CardImage={item.CardImage}
                  Author={item.Author}
                  AuthorAvatar={item.AuthorAvatar}
                  Date={item.Date}
                  BadgeText={item.BadgeText}
                  BadgeClassName={item.BadgeClassName}
                  BadgeLinkUrl={item.BadgeLinkUrl}
                  Score={item.Score}
                  Layout={item.Layout}
                />
              </div>
            ))}
            {/* --- End of Blog Card Section 2 --- */}
          </div>
        </div>
        {/* -----| End of Main Content |----- */}
      </div>
    </section>
  );
};

export default SponsoredNews;
