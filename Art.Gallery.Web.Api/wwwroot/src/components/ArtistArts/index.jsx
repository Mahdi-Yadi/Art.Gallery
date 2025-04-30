import CardItem from "../common/CardItem"

const ArtistArts = ({ Title }) => {
  const CardItemsData = [
    {
      Title: "فیلم‌های بالیوودی الگوی ضدانقلاب شده!",
      CardImage: "/src/assets/image/blog/4by3/14.jpg",
      Author: "نیلوفر راد",
      AuthorAvatar: "/src/assets/image/avatar/01.jpg",
      Date: "22 آذر، 1400",
      BadgeText: "تکنولوژی",
      BadgeClassName: "badge text-bg-warning mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "رازهای کوچک کثیف در مورد صنعت تجارت",
      CardImage: "/src/assets/image/blog/4by3/15.jpg",
      Author: "رضا کرمی",
      AuthorAvatar: "/src/assets/image/avatar/02.jpg",
      Date: "7 دی، 1400",
      BadgeText: "گردشگری",
      BadgeClassName: "badge text-bg-danger mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "عادات بدی که افراد در صنعت باید آنها را ترک کنند!",
      CardImage: "/src/assets/image/blog/4by3/16.jpg",
      Author: "سعید نوری",
      AuthorAvatar: "/src/assets/image/avatar/03.jpg",
      Date: "17 تیر، 1400",
      BadgeText: "اقتصاد",
      BadgeClassName: "badge text-bg-success mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "سال 2022 رویایی و فوق العاده برای طارمی",
      CardImage: "/src/assets/image/blog/4by3/13.jpg",
      Author: "نوید گنجی",
      AuthorAvatar: "/src/assets/image/avatar/04.jpg",
      Date: "29 مرداد، 1400",
      BadgeText: "ورزش",
      BadgeClassName: "badge text-bg-primary mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "رونمایی از طرح بزرگترین تلسکوپ نوری آسیا",
      CardImage: "/src/assets/image/blog/4by3/12.jpg",
      Author: "نگین جوان",
      AuthorAvatar: "/src/assets/image/avatar/05.jpg",
      Date: "11 دی، 1400",
      BadgeText: "تکنولوژی",
      BadgeClassName: "badge text-bg-info mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "طرح مجلس برای گرفتن مالیات از سفته بازها",
      CardImage: "/src/assets/image/blog/4by3/11.jpg",
      Author: "سارا رزاق",
      AuthorAvatar: "/src/assets/image/avatar/06.jpg",
      Date: "1 خرداد، 1400",
      BadgeText: "رسانه",
      BadgeClassName: "badge text-bg-danger mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
  ];

  return (
    <section class="position-relative pt-0">
      <div class="container">
        <div class="row">
          <div class="col-12 mb-3">
            <h2>{Title}</h2>
          </div>
          <div class="col-12">
            <div class="row gy-4">
              {CardItemsData.map((item, index) => (
                <div class="col-sm-6 col-lg-4" key={index}>
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

              <div class="col-12 text-center mt-5">
                <button type="button" class="btn btn-primary-soft">
                  مقالات بیشتر{" "}
                  <i class="bi bi-arrow-down-circle ms-2 align-middle"></i>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ArtistArts;
