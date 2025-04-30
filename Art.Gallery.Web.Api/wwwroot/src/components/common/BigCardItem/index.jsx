import Badge from "../Badge";

const BigCardItem = (Props) => {
  return (
    <div
      className="card card-overlay-bottom card-bg-scale h-400 h-lg-560"
      style={{
        backgroundImage: "url(" + Props.CardImage + ")",
        backgroundPosition: "center left",
        backgroundSize: "cover",
      }}
    >
      <div className="card-img-overlay d-flex align-items-center p-3 p-sm-5">
        <div className="w-100 mt-auto">
          <div className="col">
            <Badge
              Text={Props.BadgeText}
              className={Props.BadgeClassName}
              LinkUrl={Props.BadgeLinkUrl}
            />{" "}
            {Props.Special && (
              <a
                href="#"
                className="badge mb-2 text-bg-dark z-index-99 position-relative"
                role="button"
                data-bs-container="body"
                data-bs-toggle="popover"
                data-bs-trigger="focus"
                data-bs-placement="top"
                data-bs-content="شما این تبلیغ را می بینید زیرا فعالیت شما با مخاطبان مورد نظر سایت ما مطابقت دارد."
              >
                <i className="bi bi-info-circle pe-1"></i>
                ویژه
              </a>
            )}
            <h2 className="text-white display-6">
              <a
                href="post-single-5.html"
                className="btn-link text-reset stretched-link fw-normal"
              >
                {Props.Title}
              </a>
            </h2>
            <ul className="nav nav-divider text-white-force align-items-center d-none d-sm-inline-block">
              <li className="nav-item">
                <div className="nav-link">
                  <div className="d-flex align-items-center text-white position-relative">
                    <div className="avatar avatar-sm">
                      <img
                        className="avatar-img rounded-circle"
                        src={Props.AuthorAvatar}
                        alt="avatar"
                      />
                    </div>
                    <span className="ms-3">
                      با{" "}
                      <a
                        href="#"
                        className="stretched-link text-reset btn-link"
                      >
                        {Props.Author}
                      </a>
                    </span>
                  </div>
                </div>
              </li>
              <li className="nav-item">{Props.Date}</li>
              <li className="nav-item">
                {Props.ReadingTime} دقیقه زمان مطالعه
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BigCardItem;
