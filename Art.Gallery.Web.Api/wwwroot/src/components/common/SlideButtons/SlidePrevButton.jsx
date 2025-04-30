import { useSwiper } from 'swiper/react';

const SlidePrevButton = () => {
    const swiper = useSwiper();

  return (
    <button type="button" data-controls="prev" onClick={() => swiper.slidePrev()}>
      <i className="fas fa-chevron-right" style={{marginLeft: "2px"}}></i>
    </button>
  );
};

export default SlidePrevButton;
