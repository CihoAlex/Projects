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
@Table(name = "events")
@NamedQueries({
    @NamedQuery(name = "Events.findAll", query = "SELECT e FROM Events e"),
    @NamedQuery(name = "Events.findById", query = "SELECT e FROM Events e WHERE e.id = :id"),
    @NamedQuery(name = "Events.findByName", query = "SELECT e FROM Events e WHERE e.name = :name"),
    @NamedQuery(name = "Events.findBySize", query = "SELECT e FROM Events e WHERE e.size = :size"),
    @NamedQuery(name = "Events.findByStarttime", query = "SELECT e FROM Events e WHERE e.starttime = :starttime"),
    @NamedQuery(name = "Events.findByEndtime", query = "SELECT e FROM Events e WHERE e.endtime = :endtime"),
    @NamedQuery(name = "Events.findByScheduleid", query = "SELECT e FROM Events e WHERE e.scheduleid = :scheduleid"),
    @NamedQuery(name = "Events.findByType", query = "SELECT e FROM Events e WHERE e.type = :type")})
public class Events implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "id")
    private Integer id;
    @Column(name = "name")
    private String name;
    @Column(name = "size")
    private Integer size;
    @Column(name = "starttime")
    private Integer starttime;
    @Column(name = "endtime")
    private Integer endtime;
    @Column(name = "scheduleid")
    private Integer scheduleid;
    @Column(name = "type")
    private String type;

    public Events() {
    }

    public Events(Integer id) {
        this.id = id;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getSize() {
        return size;
    }

    public void setSize(Integer size) {
        this.size = size;
    }

    public Integer getStarttime() {
        return starttime;
    }

    public void setStarttime(Integer starttime) {
        this.starttime = starttime;
    }

    public Integer getEndtime() {
        return endtime;
    }

    public void setEndtime(Integer endtime) {
        this.endtime = endtime;
    }

    public Integer getScheduleid() {
        return scheduleid;
    }

    public void setScheduleid(Integer scheduleid) {
        this.scheduleid = scheduleid;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
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
        if (!(object instanceof Events)) {
            return false;
        }
        Events other = (Events) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "entity.Events[ id=" + id + " ]";
    }
    
}
