import { Swiper, SwiperSlide } from "swiper/react";
import ProductsCardItem from "../common/ProductsCardItem";
import SlideNextButton from "../common/SlideButtons/SlideNextButton";
import SlidePrevButton from "../common/SlideButtons/SlidePrevButton";

const Products = ({ Title }) => {
  const ProductItemsData = [
    {
      Title: "کوله پشتی ورزشی مدل کلاسیک",
      CardImage: "/assets/images/shop/02.png",
      Price: "103000 تومان",
      DiscountAmount: null,
    },
    {
      Title: "کاپشن مردانه چرم کن",
      CardImage: "/assets/images/shop/04.png",
      Price: "98000 تومان",
      DiscountAmount: null,
    },
    {
      Title: "ست 6 تکه لباس ورزشی",
      CardImage: "/assets/images/shop/08.png",
      Price: "105000 تومان",
      DiscountAmount: null,
    },
    {
      Title: "بوت زنانه چرم آرا",
      CardImage: "/assets/images/shop/06.png",
      Price: "76000 تومان",
      OldPrice: "95000 تومان",
      DiscountAmount: 20,
    },
    {
      Title: "اجاق گاز پیکنیکی مدل مخزن",
      CardImage: "/assets/images/shop/07.png",
      Price: "81000 تومان",
      DiscountAmount: null,
    },
  ];

  return (
    <section className="pt-0 pt-md-5">
      <div className="container">
        <div className="mb-4">
          <h2 className="mb-0">{Title}</h2>
        </div>

        <div className="row g-4">
          <div className="col-md-12">
            <Swiper
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

              {ProductItemsData.map((item, index) => (
                <SwiperSlide key={index}>
                  <ProductsCardItem
                        Title= {item.Title}
                        CardImage= {item.CardImage}
                        Price= {item.Price}
                        OldPrice= {item.OldPrice}
                        DiscountAmount= {item.DiscountAmount}
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

export default Products;
