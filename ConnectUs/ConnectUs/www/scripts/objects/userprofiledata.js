function Education(name, type, dateTo) {
    this.name = name;
    this.type = type;
    this.dateTo = dateTo;
}

function Work(name, type, dateFrom, dateTo, city)
{
    this.name = name; //e.g. the name of the company...
    this.type = type; //e.g. company, voluntary...
    this.dateFrom = dateFrom;
    if(dateTo)
        this.dateTo = dateTo;
    else
        this.dateTo = "curr."
    this.city = city;
}

function Interest(name, description, type) {
    this.name = name;
    this.type = type;
    this.description = description;
}