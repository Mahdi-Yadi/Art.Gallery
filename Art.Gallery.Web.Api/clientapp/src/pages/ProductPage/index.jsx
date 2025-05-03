// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import ProductDetail from "../../components/ProductDetail";
import ProductDescription from "../../components/ProductDescription";
import Products from "../../components/Products";
// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";
import "lightgallery/css/lightgallery.css";
import "lightgallery/css/lg-zoom.css";
import "lightgallery/css/lg-thumbnail.css";
import "lightgallery/css/lg-rotate.css";
import "lightgallery/css/lg-autoplay.css";
// ==============|Script Files|============== //
import "../../assets/js/functions.js";

const ProductPage = () => {
    return ( 
        <MainLayout>
            <ProductDetail/>
            <ProductDescription/>
            <Products Title={"کالاهای مشابه"}/>
        </MainLayout>
     );
}
 
export default ProductPage;