import { SignalR } from "./SignalR.js";

export class App {
    private signalR = new SignalR();

    public async initialize() {
        await this.signalR.initialize();

        const flatpickr = window["flatpickr"];

        flatpickr("[date-picker]", {
            dateFormat: "Y/m/d",
            allowInput: true,
            onChange: function (selectedDates, dateStr, instance) {
                const element = instance._input as HTMLInputElement;
                if (element != null) {
                    element.value = dateStr;
                }
            }
        });
    }
}

document.addEventListener("DOMContentLoaded", async(e) => {
    const w = new App();
    window["App"] = w;
    await w.initialize();
});

