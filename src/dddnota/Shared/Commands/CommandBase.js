module.exports = class CommandBase {
	constructor() {
		this.commandName = this.constructor.name;
	}
};