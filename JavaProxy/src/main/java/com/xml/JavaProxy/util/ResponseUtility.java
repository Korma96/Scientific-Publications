package com.xml.JavaProxy.util;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

public final class ResponseUtility {

    public static <T> ResponseEntity<T> Ok(T response) {
        return new ResponseEntity<>(response, HttpStatus.OK);
    }
}
