#!/bin/bash

SERVICE_FILE=/etc/systemd/system/kestrel-pingpong.service

if test -f "$SERVICE_FILE"; then
    systemctl start kestrel-pingpong.service
else
    cp kestrel-pingpong.service /etc/systemd/system/kestrel-pingpong.service 

   sudo -S systemctl enable kestrel-pingpong.service
fi