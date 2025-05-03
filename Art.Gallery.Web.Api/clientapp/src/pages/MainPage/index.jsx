// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import MainHero from "../../components/MainHero";
// import Highlights from "../../components/Highlights";
import Advertisement from "../../components/Advertisement";
import TopHighlights from "../../components/TopHighlights";
import TrendingTopics from "../../components/TrendingTopics";
import SportsUpdate from "../../components/SportsUpdate";
import SponsoredNews from "../../components/SponsoredNews";
import Products from "../../components/Products";
// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";
import "swiper/css";
import "swiper/css/pagination";
// ==============|Script Files|============== //
import "../../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js";
import "../../assets/js/functions.js";

const MainPage = () => {
  return (
    <MainLayout>
      <MainHero />
      <Products
        Title={"آخرین محصولات"}
        Description={"آخرین اخبار، تصاویر، فیلم ها و گزارش های ویژه"}
      />
      <Advertisement ImageSrc={"/assets/images/adv-3.png"} LinkUrl={"#"} />
      <TopHighlights
        Title={"برگزیده های هفته"}
        Description={
          "برترین های هفته گذشته، اخبار، داستان ها و اخبار سرفصل را بررسی کنید"
        }
      />
      <TrendingTopics />
      <SportsUpdate
        Title={"اخبار ورزشی"}
        Description={
          "آخرین اخبار و به‌روزرسانی‌های ورزشی را از فوتبال، تنیس، والیبال، کریکت و گلف را با نتایج و آمار زنده دریافت کنید."
        }
      />
      <SponsoredNews Title={"مطالب پیشنهادی"}/>
    </MainLayout>
  );
};

export default MainPage;
