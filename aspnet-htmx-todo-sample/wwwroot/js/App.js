export class App {
    initialize() {
        const flatpickr = window["flatpickr"];
        flatpickr("[date-picker]", {
            dateFormat: "Y/m/d",
            allowInput: true,
            onChange: function (selectedDates, dateStr, instance) {
                const element = instance._input;
                if (element != null) {
                    element.value = dateStr;
                }
            }
        });
    }
}
document.addEventListener("DOMContentLoaded", (e) => {
    const w = new App();
    window["App"] = w;
    w.initialize();
});
//# sourceMappingURL=App.js.map