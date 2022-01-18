package com.ecommerce.store.domain.dto;

import lombok.*;
import lombok.experimental.FieldDefaults;
import org.springframework.http.HttpStatus;

@Data
@AllArgsConstructor
@Builder
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class Response<T> {
    int           code;
    HttpStatus    status;
    String        message;
    T             body;
}