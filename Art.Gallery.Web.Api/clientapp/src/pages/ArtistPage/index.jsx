// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import ArtistInfo from "../../components/ArtistInfo";
import ArtistArts from "../../components/ArtistArts";
import AboutArtist from "../../components/AboutArtist";
// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";

const ArtistPage = () => {
  return (
    <MainLayout>
      <ArtistInfo
        ArtistImage={"/assets/images/avatar/02.jpg"}
        TotalArts={6}
        Name={"علیرضا اکبری"}
        City={"نیویورک"}
        Description={"در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد."}
      />
      <AboutArtist Title={"درباره علیرضا اکبری"}/>
      <ArtistArts Title={"آثار علیرضا اکبری"}/>
    </MainLayout>
  );
};

export default ArtistPage;
