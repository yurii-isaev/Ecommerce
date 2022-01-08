package com.ecommerce.store;

import com.ecommerce.store.controller.ProductController;
import com.ecommerce.store.domain.service.ProductService;
import com.ecommerce.store.entity.Product;
import org.junit.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.junit.runner.RunWith;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.reactive.WebFluxTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.context.annotation.Import;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.reactive.server.WebTestClient;
import reactor.core.publisher.*;

import java.math.BigDecimal;
import java.util.*;

import static org.mockito.ArgumentMatchers.any;

@Import(ProductService.class)
@RunWith(SpringRunner.class)
@ExtendWith(SpringExtension.class)
@WebFluxTest(controllers = ProductController.class)
public class ProductControllerTests {

    @Autowired
    WebTestClient webTestClient;

    @MockBean
    private ProductService mockService;

    @Test
    public void test_get_product_list_return_response_success() {

        // given - precondition or setup
        List<Product> productList = new ArrayList<>();

        productList.add(Product.builder()
                .brand("John")
                .price(BigDecimal.valueOf(100.00))
                .build()
        );

        productList.add(Product.builder()
                .brand("John")
                .price(BigDecimal.valueOf(100.00))
                .build()
        );

        Flux<Product> employeeFlux = Flux.fromIterable(productList);

        Mockito.when(mockService.findProductList())
                .thenReturn(employeeFlux);

        // when - action or behaviour that we are going test
        var response = webTestClient.get()
                .uri("/api/product/list")
                .accept(MediaType.APPLICATION_JSON)
                .exchange();

        // then - verify the result or output using assert statements
        response.expectStatus().isOk()
                .expectHeader().contentType(MediaType.APPLICATION_JSON)
                .expectBodyList(Product.class)
                .consumeWith(System.out::println);
    }

    @Test
    public void test_get_product_by_id_return_response_success() {

        // given - precondition or setup
        var productId = UUID.fromString("e1ebc80b-32f0-4714-851b-407a7042d5e0");

        var product = Product.builder()
                .brand("John")
                .price(BigDecimal.valueOf(100.00))
                .build();

        Mockito.when(mockService.findProductById(productId))
                .thenReturn(Mono.just(product));

        // when - action or behaviour that we are going test
        var response = webTestClient.get()
                .uri("/api/product/{id}", Collections.singletonMap("id", productId))
                .exchange();

        // then - verify the result or output using assert statements
        response.expectStatus().isOk()
                .expectBody()
                .consumeWith(System.out::println)
                .jsonPath("$.brand").isEqualTo(product.getBrand())
                .jsonPath("$.price").isEqualTo(product.getPrice());
    }

    @Test
    public void test_create_product_return_response_success() {

        // given - precondition or setup
        var productId = UUID.fromString("e1ebc80b-32f0-4714-851b-407a7042d5e0");

        Product product = Product.builder()
                .id(productId)
                .brand("test")
                .category("test")
                .description("test")
                .image("/images/product-1.jpg")
                .build();

        Mockito.when(mockService.createProduct(any(Product.class)))
                .thenReturn(Mono.just(product));

        // when - action or behaviour that we are going test
        var response = webTestClient
                .post().uri("/api/product/item")
                .contentType(MediaType.APPLICATION_JSON)
                .accept(MediaType.APPLICATION_JSON)
                .body(Mono.just(product), Product.class)
                .exchange();

        // then - verify the result or output using assert statements
        response.expectStatus().isCreated()
                .expectBody()
                .consumeWith(System.out::println)

                .jsonPath("$.brand").isEqualTo(product.getBrand())
                .jsonPath("$.category").isEqualTo(product.getCategory())
                .jsonPath("$.description").isEqualTo(product.getDescription())
                .jsonPath("$.image").isEqualTo(product.getImage());
    }
}