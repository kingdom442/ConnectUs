function Event(id, name, description, place, from, to) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.place = place;
    this.from = from;
    this.to = to;
    var now = new Date();
    if (this.from >= now && this.to <= now)
        this.state = 'Running';
    else if (this.from > now)
        this.state = 'Upcoming';
    else
        this.state = 'Finished';
}