#!/bin/bash

SERVICE_FILE=/etc/systemd/system/kestrel-pingpong.service

if test -f "$SERVICE_FILE"; then
    systemctl start kestrel-pingpong.service
else
    touch /etc/systemd/system/kestrel-pingpong.service
    echo "[Unit]
Description=PingPong Demo App running on Ubuntu

[Service]
WorkingDirectory=/var/www/pingpong
ExecStart=/usr/bin/dotnet /var/www/pingpong/PingPong.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-pingpong
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target" > /etc/systemd/system/kestrel-pingpong.service 

   systemctl start kestrel-pingpong.service
fi