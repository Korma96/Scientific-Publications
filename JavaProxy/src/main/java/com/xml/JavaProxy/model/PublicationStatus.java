package com.xml.JavaProxy.model;

public enum PublicationStatus {
    WAITING,
    REJECTED,
    WITHDRAWN,
    PUBLISHED;


    public String value() {
        return name();
    }

    public static PublicationStatus fromValue(String v) {
        return valueOf(v);
    }

    }
