export class SignalR {
    connection;
    async initialize() {
        const signalR = window["signalR"];
        this.connection = new signalR.HubConnectionBuilder().withUrl("/MyHub").build();
        this.connection.on("HtmxTrigger", this.htmxTrigger.bind(this));
        await this.connection.start();
    }
    htmxTrigger(selector, eventName) {
        const htmx = window["htmx"];
        htmx.trigger(selector, eventName);
    }
}
//# sourceMappingURL=SignalR.js.map