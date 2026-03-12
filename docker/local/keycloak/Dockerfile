FROM alpine:3.20 AS installer

RUN apk add --no-cache curl ca-certificates

FROM quay.io/keycloak/keycloak:26.5
COPY --from=installer /usr/bin/curl /usr/bin/curl
COPY --from=installer /lib/ld-musl-x86_64.so.1 /lib/ld-musl-x86_64.so.1
COPY --from=installer /usr/lib/libcurl.so.4 /usr/lib/libcurl.so.4
COPY --from=installer /usr/lib/libnghttp2.so.14 /usr/lib/libnghttp2.so.14
COPY --from=installer /usr/lib/libssl.so.3 /usr/lib/libssl.so.3
COPY --from=installer /usr/lib/libcrypto.so.3 /usr/lib/libcrypto.so.3
COPY --from=installer /usr/lib/libbrotlidec.so.1 /usr/lib/libbrotlidec.so.1
COPY --from=installer /usr/lib/libbrotlicommon.so.1 /usr/lib/libbrotlicommon.so.1
COPY --from=installer /lib/libz.so.1 /lib/libz.so.1
COPY --from=installer /usr/lib/libcares.so.2 /usr/lib/libcares.so.2
COPY --from=installer /usr/lib/libidn2.so.0 /usr/lib/libidn2.so.0
COPY --from=installer /usr/lib/libpsl.so.5 /usr/lib/libpsl.so.5
COPY --from=installer /usr/lib/libzstd.so.1 /usr/lib/libzstd.so.1
COPY --from=installer /usr/lib/libunistring.so.5 /usr/lib/libunistring.so.5

RUN /opt/keycloak/bin/kc.sh --health-enabled=true --db=mysql --features=opentelemetry-logs --telemetry-logs-enabled=true build

USER 1000