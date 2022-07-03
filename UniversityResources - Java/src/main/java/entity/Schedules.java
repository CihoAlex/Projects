/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package entity;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;

/**
 *
 * @author alexc
 */
@Entity
@Table(name = "schedules")
@NamedQueries({
    @NamedQuery(name = "Schedules.findAll", query = "SELECT s FROM Schedules s"),
    @NamedQuery(name = "Schedules.findById", query = "SELECT s FROM Schedules s WHERE s.id = :id"),
    @NamedQuery(name = "Schedules.findBySchedule", query = "SELECT s FROM Schedules s WHERE s.schedule = :schedule"),
    @NamedQuery(name = "Schedules.findByEventname", query = "SELECT s FROM Schedules s WHERE s.eventname = :eventname"),
    @NamedQuery(name = "Schedules.findByEventsize", query = "SELECT s FROM Schedules s WHERE s.eventsize = :eventsize"),
    @NamedQuery(name = "Schedules.findByEventstart", query = "SELECT s FROM Schedules s WHERE s.eventstart = :eventstart"),
    @NamedQuery(name = "Schedules.findByEventend", query = "SELECT s FROM Schedules s WHERE s.eventend = :eventend"),
    @NamedQuery(name = "Schedules.findByRoom", query = "SELECT s FROM Schedules s WHERE s.room = :room"),
    @NamedQuery(name = "Schedules.findByRoomcapacity", query = "SELECT s FROM Schedules s WHERE s.roomcapacity = :roomcapacity"),
    @NamedQuery(name = "Schedules.findByTime", query = "SELECT s FROM Schedules s WHERE s.time = :time")})
public class Schedules implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "id")
    private Integer id;
    @Column(name = "schedule")
    private Integer schedule;
    @Column(name = "eventname")
    private String eventname;
    @Column(name = "eventsize")
    private String eventsize;
    @Column(name = "eventstart")
    private Integer eventstart;
    @Column(name = "eventend")
    private Integer eventend;
    @Column(name = "room")
    private String room;
    @Column(name = "roomcapacity")
    private Integer roomcapacity;
    @Column(name = "time")
    private Integer time;

    public Schedules() {
    }

    public Schedules(Integer id) {
        this.id = id;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getSchedule() {
        return schedule;
    }

    public void setSchedule(Integer schedule) {
        this.schedule = schedule;
    }

    public String getEventname() {
        return eventname;
    }

    public void setEventname(String eventname) {
        this.eventname = eventname;
    }

    public String getEventsize() {
        return eventsize;
    }

    public void setEventsize(String eventsize) {
        this.eventsize = eventsize;
    }

    public Integer getEventstart() {
        return eventstart;
    }

    public void setEventstart(Integer eventstart) {
        this.eventstart = eventstart;
    }

    public Integer getEventend() {
        return eventend;
    }

    public void setEventend(Integer eventend) {
        this.eventend = eventend;
    }

    public String getRoom() {
        return room;
    }

    public void setRoom(String room) {
        this.room = room;
    }

    public Integer getRoomcapacity() {
        return roomcapacity;
    }

    public void setRoomcapacity(Integer roomcapacity) {
        this.roomcapacity = roomcapacity;
    }

    public Integer getTime() {
        return time;
    }

    public void setTime(Integer time) {
        this.time = time;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (id != null ? id.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Schedules)) {
            return false;
        }
        Schedules other = (Schedules) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "entity.Schedules[ id=" + id + " ]";
    }
    
}
