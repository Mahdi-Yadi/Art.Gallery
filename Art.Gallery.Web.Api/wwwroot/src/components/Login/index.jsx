import { useActionState } from "react";
import { Link } from "react-router";
import InputField from "../InputField";
import CustomButton from "../common/CustomButton";

const Login = () => {
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
      <div className="container">
        <div className="row">
          <div className="col-md-10 col-lg-8 col-xl-6 mx-auto">
            <div className="p-4 p-sm-5 bg-primary bg-opacity-10 rounded">
              <h2>ورود به حساب کاربری</h2>
              {/* -----| Form |----- */}
              <form action={submitAction} className="mt-4">
                {/* --- Email --- */}
                <div className="mb-3">
                  <InputField
                    Label={"پست الکترونیک"}
                    Type={"email"}
                    Name={"exampleInputEmail1"}
                    ID={"exampleInputEmail1"}
                    Placeholder={"example@sample.com"}
                    isPending={isPending}
                  />
                </div>
                {/* --- End of Email --- */}

                {/* --- Password --- */}
                <div className="mb-3">
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

                {/* --- Checkbox --- */}
                <div className="mb-3 form-check">
                  <input
                    type="checkbox"
                    className="form-check-input"
                    id="exampleCheck1"
                  />
                  <label className="form-check-label" htmlFor="exampleCheck1">
                    مرا به خاطر بسپار
                  </label>
                </div>
                {/* --- End of Checkbox --- */}

                {/* --- Button --- */}
                <div className="row align-items-center">
                  <div className="col-sm-4">
                    <button
                      type="submit"
                      className="btn btn-success"
                      disabled={isPending}
                    >
                      ورود{" "}
                    </button>
                  </div>
                  <div className="col-sm-8 text-sm-end">
                    <span>
                      آیا هنوز ثبت نام نکرده اید؟{" "}
                      <Link to={"/Register"}>
                        <u>ثبت نام</u>
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

export default Login;
