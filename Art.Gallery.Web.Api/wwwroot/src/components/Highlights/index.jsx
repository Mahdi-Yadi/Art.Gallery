import { Swiper, SwiperSlide } from "swiper/react";
import { Autoplay } from "swiper/modules";
import CardItem from "../common/CardItem";
import SlideNextButton from "../common/SlideButtons/SlideNextButton";
import SlidePrevButton from "../common/SlideButtons/SlidePrevButton";

const Highlights = ({ Title, Description }) => {
  const CardItemsData = [
    {
      Title: "افزایش آلودگی هوا در شهرهای پُرجمعیت تا فردا",
      CardImage: "src/assets/image/blog/4by3/07.jpg",
      Author: "مریم ترابی",
      AuthorAvatar: "src/assets/image/avatar/07.jpg",
      Date: "7 دی، 1402",
      BadgeText: "گردشگری",
      BadgeClassName: "badge text-bg-info mb-2",
      BadgeLinkUrl: "#",
      Score: 8.5,
      Layout: "Vertical",
    },
    {
      Title: "آمار فرزندان حاصل از روش‌های کمک‌ باروری در جهان",
      CardImage: "src/assets/image/blog/4by3/08.jpg",
      Author: "رضا مرادی",
      AuthorAvatar: "src/assets/image/avatar/02.jpg",
      Date: "15 خرداد، 1400",
      BadgeText: "ورزش",
      BadgeClassName: "badge text-bg-danger mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "عادات بدی که افراد در صنعت باید آنها را ترک کنند!",
      CardImage: "src/assets/image/blog/4by3/09.jpg",
      Author: "سارا حقیقت نژاد",
      AuthorAvatar: "src/assets/image/avatar/09.jpg",
      Date: "1 دی، 1400",
      BadgeText: "سیاست",
      BadgeClassName: "badge text-bg-success mb-2",
      BadgeLinkUrl: "#",
      Score: 5,
      Layout: "Vertical",
    },
    {
      Title: "به همین دلیل امسال سال استارت آپ ها خواهد بود؟",
      CardImage: "src/assets/image/blog/4by3/10.jpg",
      Author: "نیلوفر راد",
      AuthorAvatar: "src/assets/image/avatar/10.jpg",
      Date: "7 آذر، 1400",
      BadgeText: "فرهنگ",
      BadgeClassName: "badge text-bg-primary mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
    {
      Title: "بهترین تابلوهای پینترست برای یادگیری در مورد تجارت",
      CardImage: "src/assets/image/blog/4by3/11.jpg",
      Author: "المیرا کرمی",
      AuthorAvatar: "src/assets/image/avatar/12.jpg",
      Date: "7 شهریور، 1400",
      BadgeText: "تکنولوژی",
      BadgeClassName: "badge text-bg-warning mb-2",
      BadgeLinkUrl: "#",
      Score: 0,
      Layout: "Vertical",
    },
  ];

  return (
    <section className="pt-4">
      <div className="container">
        <div className="row">
          <div className="col-md-12">
            <div className="mb-4">
              <h2 className="m-0">
                <i className="bi bi-megaphone me-2"></i>
                {Title}
              </h2>
              <p className="m-0">{Description}</p>
            </div>
            <Swiper
              modules={[Autoplay]}
              autoplay={{
                delay: 4000,
                disableOnInteraction: false,
              }}
              speed={1000}
              slidesPerView={4}
              loop={true}
              spaceBetween={24}
              breakpoints={{
                360: {
                  slidesPerView: 1,
                  spaceBetween: 24,
                },
                576: {
                  slidesPerView: 2,
                  spaceBetween: 24,
                },
                768: {
                  slidesPerView: 3,
                  spaceBetween: 24,
                },
                992: {
                  slidesPerView: 4,
                  spaceBetween: 24,
                },
              }}
              className="arrow-hover"
            >
              <div className="tns-controls arrow-blur arrow-dark arrow-round">
                <SlideNextButton />
                <SlidePrevButton />
              </div>

              {CardItemsData.map((item, index) => (
                <SwiperSlide key={index}>
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
                </SwiperSlide>
              ))}
            </Swiper>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Highlights;
