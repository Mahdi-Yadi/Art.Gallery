// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import NotFound from "../../components/NotFound";

// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";
// ==============|Script Files|============== //

const NotFoundPage = () => {
  return (
    <MainLayout>
      <NotFound/>
    </MainLayout>
  );
};

export default NotFoundPage;
