import { Swiper, SwiperSlide } from "swiper/react";
import { useMediaQuery } from "react-responsive";
import SlideNextButton from "../common/SlideButtons/SlideNextButton";
import SlidePrevButton from "../common/SlideButtons/SlidePrevButton";

const TrendingTopics = () => {
  const CategoryItemsData = [
    {
      Title: "گردشگری",
      CardImage: "src/assets/image/blog/1by1/thumb/01.jpg",
    },
    {
      Title: "اقتصاد",
      CardImage: "src/assets/image/blog/1by1/thumb/02.jpg",
    },
    {
      Title: "تکنولوژی",
      CardImage: "src/assets/image/blog/1by1/thumb/03.jpg",
    },
    {
      Title: "فرهنگ",
      CardImage: "src/assets/image/blog/1by1/thumb/04.jpg",
    },
    {
      Title: "ورزش",
      CardImage: "src/assets/image/blog/1by1/thumb/05.jpg",
    },
  ];

  const isTabletOrMobile = useMediaQuery({ query: "(max-width: 1200px)" });

  return (
    <section className="p-0">
      <div className="container">
        <div className="row g-0">
          <div className="col-12 bg-light p-2 p-sm-4 rounded-3">
            <div className="d-flex justify-content-between align-items-center mb-4">
              <h2 className="m-0">دسته بندی ها</h2>
              <a href="#" className="text-body text-primary-hover">
                <u>مشاهده همه</u>
              </a>
            </div>

            <Swiper
              speed={1000}
              loop={true}
              slidesPerView={5}
              spaceBetween={24}
              breakpoints={{
                360: {
                  slidesPerView: 2,
                },
                576: {
                  slidesPerView: 2,
                },
                768: {
                  slidesPerView: 3,
                },
                992: {
                  slidesPerView: 4,
                },
                1200: {
                  slidesPerView: 5,
                },
              }}
              className="arrow-hover"
            >
              {isTabletOrMobile && (
                <div className="tns-controls arrow-blur arrow-dark arrow-round">
                  <SlideNextButton />
                  <SlidePrevButton />
                </div>
              )}

              {CategoryItemsData.map((item, index) => (
                <SwiperSlide key={index}>
                  <div className="card card-overlay-bottom card-img-scale">
                    <img
                      className="card-img"
                      src={item.CardImage}
                      alt="card image"
                    />
                    <div className="card-img-overlay d-flex px-3 px-sm-5">
                      <h5 className="mt-auto mx-auto">
                        <a
                          href="#"
                          className="stretched-link btn-link text-white"
                        >
                          {item.Title}
                        </a>
                      </h5>
                    </div>
                  </div>
                </SwiperSlide>
              ))}
            </Swiper>
          </div>
        </div>
      </div>
    </section>
  );
};

export default TrendingTopics;
