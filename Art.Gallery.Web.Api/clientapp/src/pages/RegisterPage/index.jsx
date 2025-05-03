// ==============|Components|============== //
import MainLayout from "../../app/layout/MainLayout";
import Register from "../../components/Register";
// ==============|Css Files|============== //
import "../../assets/vendor/font-awesome/css/all.min.css";
import "../../assets/vendor/bootstrap-icons/bootstrap-icons.css";
// ==============|Script Files|============== //
import "../../assets/js/functions.js";

const RegisterPage = () => {
    return ( 
        <MainLayout>
            <Register/>
        </MainLayout>
     );
}
 
export default RegisterPage;