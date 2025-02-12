export class SignalR {
    private connection: any;

    public async initialize() {
        const signalR = window["signalR"];
        this.connection = new signalR.HubConnectionBuilder().withUrl("/MyHub").build();
        this.connection.on("HtmxTrigger", this.htmxTrigger.bind(this));
        await this.connection.start();
    }
    private htmxTrigger(selector: string, eventName: string) {
        const htmx = window["htmx"];
        htmx.trigger(selector, eventName);
    }
}