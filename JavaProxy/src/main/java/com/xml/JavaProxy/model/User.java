package com.xml.JavaProxy.model;

import javax.xml.bind.annotation.*;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
        "username",
        "password",
        "salt",
        "role",
        "name",
        "email",
        "city",
        "country"
})
@XmlRootElement(name = "user", namespace = "http://ftn.uns.ac.rs/xml2019/user")
public class User {

    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String username;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String password;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String salt;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String role;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String name;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String email;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String city;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/user", required = true)
    private String country;

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getSalt() {
        return salt;
    }

    public void setSalt(String salt) {
        this.salt = salt;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }
}
