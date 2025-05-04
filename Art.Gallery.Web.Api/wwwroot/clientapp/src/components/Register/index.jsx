import { useActionState } from "react";
import { Link } from "react-router";
import InputField from "../InputField";
import CustomButton from "../common/CustomButton";

const Register = () => {
    const [error, submitAction, isPending] = useActionState(async () => {
      await new Promise( r =>
        setTimeout(r, 3000)
      );
  
      if (error) {
        return error;
      }
    });

  return (
    <section>
      <div class="container">
        <div class="row">
          <div class="col-md-10 col-lg-8 col-xl-6 mx-auto">
            <div class="bg-primary bg-opacity-10 rounded p-4 p-sm-5">
              <h2>ثبت نام در سایت </h2>
              {/* -----| Form |----- */}
              <form action={submitAction} class="mt-4">
                {/* --- Email --- */}
                <div class="mb-3">
                  <InputField
                    Label={"پست الکترونیک"}
                    Type={"email"}
                    Name={"exampleInputEmail1"}
                    ID={"exampleInputEmail1"}
                    Placeholder={"example@sample.com"}
                    HelpText={
                      "ما هرگز ایمیل شما را با دیگران به اشتراک نمی گذاریم."
                    }
                    isPending={isPending}
                  />
                </div>
                {/* --- End of Email --- */}

                {/* --- Password --- */}
                <div class="mb-3">
                  <InputField
                    Label={"رمز عبور"}
                    Type={"password"}
                    Name={"exampleInputPassword1"}
                    ID={"exampleInputPassword1"}
                    Placeholder={"*********"}
                    isPending={isPending}
                  />
                </div>
                {/* --- End of Password --- */}

                {/* --- ReEnterPassword --- */}
                <div class="mb-3">
                  <InputField
                    Label={"تایید رمز عبور"}
                    Type={"password"}
                    Name={"exampleReEnterInputPassword1"}
                    ID={"exampleReEnterInputPassword1"}
                    Placeholder={"*********"}
                    isPending={isPending}
                  />
                </div>
                {/* --- End of ReEnterPassword --- */}

                {/* --- Checkbox --- */}
                <div class="mb-3 form-check">
                  <input
                    type="checkbox"
                    class="form-check-input"
                    id="exampleCheck1"
                  />
                  <label class="form-check-label" for="exampleCheck1">
                    من می خواهم برای عضویت در خبرنامه نیز ثبت نام کنم
                  </label>
                </div>
                {/* --- End of Checkbox --- */}

                {/* --- Button --- */}
                <div class="row align-items-center">
                  <div class="col-sm-4">
                    <button type="submit" class="btn btn-success" disabled={isPending}>
                      ثبت نام
                    </button>
                  </div>
                  <div class="col-sm-8 text-sm-end">
                    <span>
                      آیا قبلا ثبت نام کرده اید؟{" "}
                      <Link to={"/Login"}>
                        <u>ورود</u>
                      </Link>
                    </span>
                  </div>
                </div>
                {/* --- End of Button --- */}
              </form>
              {/* -----| End of Form |----- */}
              <hr />
              {/* -----| Social Media Buttons |----- */}
              <div className="text-center">
                <p>برای دسترسی سریع با شبکه اجتماعی خود وارد شوید</p>
                <ul className="list-unstyled d-sm-flex mt-3 justify-content-center">
                  {/* --- Facebook --- */}
                  <li className="mx-2">
                    <CustomButton
                      Icon={"fab fa-facebook-f"}
                      Text={"ورود با Facebook"}
                      className={"btn bg-facebook d-inline-block"}
                      Link={"#"}
                    />
                  </li>
                  {/* --- End of Facebook --- */}

                  {/* --- Google --- */}
                  <li className="mx-2">
                    <CustomButton
                      Icon={"fab fa-google"}
                      Text={"ورود با Google"}
                      className={"btn bg-google d-inline-block"}
                      Link={"#"}
                    />
                  </li>
                  {/* --- End of Google --- */}
                </ul>
              </div>
              {/* -----| End of Social Media Buttons |----- */}
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Register;
