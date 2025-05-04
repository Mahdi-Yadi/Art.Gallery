const InputField = (Props) => {
  function CheckLabel() {
    if (Props.Label) {
      return (
        <label className="form-label" htmlFor={Props.ID}>
          {Props.Label}
        </label>
      );
    }
  }

  function CheckHelp() {
    if (Props.HelpText) {
      return (
        <small id={Props.Name + "Help"} class="form-text">
          {Props.HelpText}
        </small>
      );
    }
  }

  return (
    <>
      {CheckLabel()}
      <input
        type={Props.Type}
        className="form-control"
        name={Props.Name}
        id={Props.ID}
        placeholder={Props.Placeholder}
        disabled={Props.isPending}
        aria-describedby={Props.Name + "Help"}
      />
      {CheckHelp()}
    </>
  );
};

export default InputField;
