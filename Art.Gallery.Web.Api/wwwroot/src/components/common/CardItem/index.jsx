// ==============|Components|============== //
import Badge from "../Badge";

// ==============|Functions|============== //
function CheckScore(Score) {
  if (Score > 0) {
    return (
      <div
        className="icon-md bg-white bg-opacity-10 bg-blur text-white fw-bold rounded-circle"
        title={Score + "rating"}
      >
        {Score}
      </div>
    );
  } else {
    return;
  }
}

function CheckLayout(Props) {
  if (Props.Layout === "Vertical") {
    return (
      <div className="card">
        <div className="position-relative">
          <img className="card-img" src={Props.CardImage} alt="Card image" />
          <div className="card-img-overlay d-flex align-items-start flex-column p-3">
            <div className="w-100 mb-auto d-flex justify-content-end">
              <div className="text-end ms-auto">{CheckScore(Props.Score)}</div>
            </div>

            <div className="w-100 mt-auto">
              <Badge
                Text={Props.BadgeText}
                className={Props.BadgeClassName}
                LinkUrl={Props.BadgeLinkUrl}
              />
            </div>
          </div>
        </div>
        <div className="card-body px-0 pt-3">
          <h5 className="card-title">
            <a href="post-single-3.html" className="btn-link text-reset">
              {Props.Title}
            </a>
          </h5>

          <ul className="nav nav-divider align-items-center d-none d-sm-inline-block">
            <li className="nav-item">
              <div className="nav-link">
                <div className="d-flex align-items-center position-relative">
                  <div className="avatar avatar-xs">
                    <img
                      className="avatar-img rounded-circle"
                      src={Props.AuthorAvatar}
                      alt="avatar"
                    />
                  </div>
                  <span className="ms-3">
                    با{" "}
                    <a href="#" className="stretched-link text-reset btn-link">
                      {Props.Author}
                    </a>
                  </span>
                </div>
              </div>
            </li>
            <li className="nav-item">{Props.Date}</li>
          </ul>
        </div>
      </div>
    );
  } else if (Props.Layout === "Horizontal") {
    return (
      <div className="card mb-2 mb-md-4">
        <div className="row g-3">
          <div className="col-4">
            <img
              className="rounded-3"
              src={Props.CardImage}
              alt=""
            />
          </div>
          <div className="col-8">
            <Badge
              Text={Props.BadgeText}
              className={Props.BadgeClassName}
              LinkUrl={Props.BadgeLinkUrl}
            />
            <h5>
              <a
                href="post-single-5.html"
                className="btn-link stretched-link text-reset"
              >
              {Props.Title}
              </a>
            </h5>
            <ul className="nav nav-divider align-items-center d-none d-sm-inline-block">
              <li className="nav-item">
                <div className="nav-link">
                  <div className="d-flex align-items-center position-relative">
                    <div className="avatar avatar-xs">
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
            </ul>
          </div>
        </div>
      </div>
    );
  }
}

const CardItem = (Props) => {
  return <>{CheckLayout(Props)}</>;
};

export default CardItem;
