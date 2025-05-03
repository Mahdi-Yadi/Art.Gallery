const ArtistInfo = (Props) => {
  return (
    <section class="pt-4">
      <div class="container">
        <div class="row">
          <div class="col-12">
            <div class="bg-primary bg-opacity-10 d-md-flex p-3 p-sm-4 my-3 text-center text-md-start rounded">
              <div class=" me-0 me-md-4 mb-3 mb-md-0">
                <div class="avatar avatar-xxl">
                  <img
                    class="avatar-img rounded-circle"
                    src={Props.ArtistImage}
                    alt="avatar"
                  />
                </div>

                <div class="text-center mt-n3 position-relative">
                  <span class="badge bg-danger fs-6">{Props.TotalArts + " آثار"}</span>
                </div>
              </div>

              <div>
                <h2>{Props.Name}</h2>
                <ul class="list-inline">
                  {/* <li class="list-inline-item">
                    <i class="bi bi-person-fill me-1"></i> 
                    {Props.Role}
                  </li> */}
                  <li class="list-inline-item">
                    <i class="bi bi-geo-alt me-1"></i> 
                    {Props.City}
                  </li>
                </ul>
                <p class="my-2">
                  {Props.Description}
                </p>

                <ul class="nav justify-content-center justify-content-md-start">
                  <li class="nav-item">
                    <a class="nav-link ps-0 pe-2 fs-5" href="#">
                      <i class="fab fa-facebook-square"></i>
                    </a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link px-2 fs-5" href="#">
                      <i class="fab fa-twitter-square"></i>
                    </a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link px-2 fs-5" href="#">
                      <i class="fab fa-linkedin"></i>
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ArtistInfo;
