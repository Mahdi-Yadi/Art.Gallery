import { useEffect, useRef } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Pagination, Keyboard } from "swiper/modules";
import lightGallery from "lightgallery";
import lgThumbnail from "lightgallery/plugins/thumbnail";
import lgZoom from "lightgallery/plugins/zoom";
import lgRotate from "lightgallery/plugins/rotate";
import lgAutoplay from "lightgallery/plugins/autoplay";

const ProductGallery = () => {
  const images = [
    {
      thumb: "/assets/images/shop/11.png",
      full: "/assets/images/shop/11.png",
      size: "1600-1200",
    },
    {
      thumb: "/assets/images/shop/13.png",
      full: "/assets/images/shop/13.png",
      size: "1600-1200",
    },
    {
      thumb: "/assets/images/shop/14.png",
      full: "/assets/images/shop/14.png",
      size: "1600-1200",
    },
  ];

  const swiperInstance = useRef(null);
  const galleryRef = useRef(null);

  useEffect(() => {
    if (!galleryRef.current) return;

    const lg = lightGallery(galleryRef.current, {
      selector: "a",
      plugins: [lgThumbnail, lgZoom, lgAutoplay, lgRotate],
      speed: 500,
      licenseKey: "0000-0000-0000-0000",
    });

    galleryRef.current.addEventListener("lgBeforeClose", () => {
      if (swiperInstance.current) {
        swiperInstance.current.slideTo(lg.index, 0);
      }
    });

    return () => {
      if (lg) lg.destroy();
    };
  }, []);

  return (
    <div ref={galleryRef}>
      <Swiper
        onSwiper={(swiper) => (swiperInstance.current = swiper)}
        modules={[Pagination, Keyboard]}
        spaceBetween={10}
        slidesPerView={1}
        pagination={{ clickable: true, dynamicBullets: true }}
        keyboard={{ enabled: true }}
      >
        {images.map((img, index) => (
          <SwiperSlide key={index}>
            <a
              href={img.full}
              data-lg-size={img.size}
            >
              <img
                src={img.thumb}
                alt={`تصویر ${index + 1}`}
              />
            </a>
          </SwiperSlide>
        ))}
      </Swiper>
    </div>
  );
};

export default ProductGallery;
