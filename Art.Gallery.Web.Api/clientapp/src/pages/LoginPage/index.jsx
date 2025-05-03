// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import Login from "../../components/Login";
// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";
// ==============|Script Files|============== //
import "../../assets/js/functions.js";

const LoginPage = () => {
    return ( 
        <MainLayout>
            <Login/>
        </MainLayout>
     );
}
 
export default LoginPage;