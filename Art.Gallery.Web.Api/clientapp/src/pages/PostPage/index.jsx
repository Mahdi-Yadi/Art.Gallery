// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import PostContent from "../../components/PostContent/index.jsx";
// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";
// ==============|Script Files|============== //
import "../../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js";

const PostPage = () => {
    return ( 
        <MainLayout>
            <PostContent/>
        </MainLayout>
     );
}
 
export default PostPage;