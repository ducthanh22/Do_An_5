# Sử dụng hình ảnh Apache cơ bản từ Docker Hub
FROM httpd:2.4

# Sao chép tệp localhost.crt và localhost.key vào container
COPY localhost.crt /usr/local/apache2/conf/localhost.crt
COPY localhost.key /usr/local/apache2/conf/localhost.key

# Sao chép tệp cấu hình Apache và Swagger UI vào container
COPY swagger.conf /usr/local/apache2/conf/swagger.conf
COPY swagger.json /usr/local/apache2/htdocs/swagger.json

# Cấu hình Apache để sử dụng chứng chỉ SSL và Swagger UI
RUN echo "Listen 443" >> /usr/local/apache2/conf/httpd.conf
RUN echo "LoadModule ssl_module modules/mod_ssl.so" >> /usr/local/apache2/conf/httpd.conf
RUN echo "SSLEngine on" >> /usr/local/apache2/conf/httpd.conf
RUN echo "Include conf/swagger.conf" >> /usr/local/apache2/conf/httpd.conf
