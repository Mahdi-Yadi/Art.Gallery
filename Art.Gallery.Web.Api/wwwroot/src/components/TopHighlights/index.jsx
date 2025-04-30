import CardItem from "../common/CardItem";
import BigCardItem from "../common/BigCardItem";

const TopHighlights = ({ Title, Description }) => {
  const CardItemsData = [
    {
      Title: "خرید و فروش ارز در کانال 37 هزار تومانی",
      CardImage: "src/assets/image/blog/4by3/01.jpg",
      Author: "دانیال بابایی",
      AuthorAvatar: "src/assets/image/avatar/03.jpg",
      Date: "28 آذر، 1402",
      BadgeText: "مگامنو",
      BadgeClassName: "badge bg-danger bg-opacity-10 text-danger mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "کاهش 192 هزار میلیارد تومانی بدهی دولت",
      CardImage: "src/assets/image/blog/4by3/02.jpg",
      Author: "مسعود راد",
      AuthorAvatar: "src/assets/image/avatar/02.jpg",
      Date: "7 دی، 1400",
      BadgeText: "مگامنو",
      BadgeClassName: "badge bg-info bg-opacity-10 text-info mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "ساخت مسکن موتور محرک کاهش تورم",
      CardImage: "src/assets/image/blog/4by3/03.jpg",
      Author: "مریم حسنی",
      AuthorAvatar: "src/assets/image/avatar/01.jpg",
      Date: "17 تیر، 1400",
      BadgeText: "اقتصاد",
      BadgeClassName: "badge bg-success bg-opacity-10 text-success mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
    {
      Title: "آشنایی با کلید موفقیت نهضت ملی مسکن‌",
      CardImage: "src/assets/image/blog/4by3/04.jpg",
      Author: "مهسا عظیمی",
      AuthorAvatar: "src/assets/image/avatar/05.jpg",
      Date: "17 تیر، 1400",
      BadgeText: "تکنولوژی",
      BadgeClassName: "badge bg-warning bg-opacity-15 text-warning mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Horizontal",
    },
  ];

  return (
    <section>
      <div className="container">
        <div className="row">
          <div className="col">
            <div className="mb-4">
              <h2 className="m-0">
                <i className="bi bi-bookmark-star me-2"></i>
                {Title}
              </h2>
              <p className="m-0">{Description}</p>
            </div>
            <div className="row gy-4">
              <div className="col-lg-7">

                <BigCardItem
                Title={"رازهای کوچک کثیف در مورد صنعت تجارت"}
                CardImage={"src/assets/image/blog/16by9/05.jpg"}
                Author={"ادمین"}
                AuthorAvatar={"src/assets/image/avatar/01.jpg"}
                Date={"8 دی، 1400"}
                BadgeText={"پزشکی"}
                BadgeClassName={"badge text-bg-primary mb-2"}
                BadgeLinkUrl={"#"}
                ReadingTime={3}
                Special={true}
                />
                
              </div>
              <div className="col-lg-5">

                {CardItemsData.map((item, index) => (
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

              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default TopHighlights;
