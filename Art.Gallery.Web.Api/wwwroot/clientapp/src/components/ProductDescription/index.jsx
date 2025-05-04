const ProductDescription = () => {
  const ProductItemsData = [
    {
      Description: ""
    },
  ];

  return (
    <section className="pt-0 pt-lg-5">
      <div className="container">
        <div className="row g-4 justify-content-between">
          <div className="col-lg-7">
            <h5>توضیحات</h5>
            <p>
              متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای
              شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود
              ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته،
              حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم
              افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان
              خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد.
            </p>
            <p>
              <b>زمان پردازش:</b> چاپگرها و متون بلکه روزنامه و مجله در ستون و
              سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و
              کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد.
            </p>
            <p>
              <b>زمان ارسال:</b> با نرم افزارها شناخت بیشتری را برای طراحان
              رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی
              ایجاد کرد.
            </p>
            <p>
              ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل
              حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود
              طراحی اساسا مورد استفاده قرار گیرد.
            </p>
          </div>

          <div className="col-lg-4">
            <h5>فروشنده</h5>
            <div className="d-flex align-items-center position-relative">
              <div className="avatar avatar-sm">
                <img
                  className="avatar-img rounded-circle"
                  src="/src/assets/image/avatar/01.jpg"
                  alt="avatar"
                />
              </div>
              <div className="ms-3">
                <h6 className="mb-0">
                  با{" "}
                  <a href="#" className="stretched-link text-reset btn-link">
                    نازنین مرادی
                  </a>
                </h6>
              </div>
            </div>

            <div className="hstack gap-3 flex-wrap mt-2 mb-4">
              <p className="mb-0">
                <i className="bi bi-star-fill text-warning"></i> 5.0{" "}
                <span>رضایت از کالا</span>
              </p>
              <p className="mb-0">
                <i className="bi bi-patch-check-fill text-primary"></i> 99%{" "}
                <span>پیشنهاد خرید</span>
              </p>
            </div>

            <h5>اطلاعات حمل و نقل</h5>
            <ul className="list-group list-group-borderless">
              <li className="list-group-item py-0">
                <span>حمل و نقل:</span>
                <span className="h6 mb-0">
                  حمل و نقل بین المللی طولانی مدت رایگان
                </span>
              </li>
              <li className="list-group-item pb-0">
                <span>تحویل:</span>
                <span className="h6 mb-0">ارسال از 1 روز کاری دیگر</span>
              </li>
              <li className="list-group-item pb-0">
                <span>پرداخت:</span>
                <img
                  src="/src/assets/image/icon/zarinpal.svg"
                  className="w-90 me-2 p-1 rounded-2"
                  alt=""
                  style={{backgroundColor: "#191a1f"}}
                />
              </li>
              <li className="list-group-item pb-0">
                <span>بازگشت:</span>
                <span className="h6 mb-0">ضمانت مرجوعی کالا تا یک هفته</span>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ProductDescription;
