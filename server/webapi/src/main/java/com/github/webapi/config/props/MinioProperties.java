package com.github.webapi.config.props;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

@ConfigurationProperties(prefix = "minio")
@Component
@Data
public class MinioProperties {
   private String accessKey;
   private String bucket;
   private String secretKey;
   private String url;
}
