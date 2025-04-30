import { useSwiper } from 'swiper/react';

const SlideNextButton = () => {
    const swiper = useSwiper();
    
  return (
    <button type="button" data-controls="next" onClick={() => swiper.slideNext()}>
      <i className="fas fa-chevron-left" style={{marginRight: "2px"}}></i>
    </button>
  );
};

export default SlideNextButton;
