#!/bin/bash
SERVICE_FILE=/etc/systemd/system/kestrel-pingpong.service
if test -f "$SERVICE_FILE"; then
    sudo systemctl stop kestrel-pingpong.service
fi