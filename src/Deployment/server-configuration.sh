#!/bin/bash

CURRENT_TIME=date +"%Y_%m_%d_%H_%M"

BACKUP_FOLDER=~/config_backup

PROXY_CONF=/etc/nginx/proxy.conf
NGINX_CONF=/etc/nginx/nginx.conf
SITE_CONF=/etc/nginx/sites-enabled/pingpong-site
SERVICE_FILE=/etc/systemd/system/kestrel-pingpong.service

if [ ! -d "$BACKUP_FOLDER" ]; then
    mkdir $BACKUP_FOLDER
fi

if [ -f "$PROXY_CONF" ]; then
    cp $PROXY_CONF $BACKUP_FOLDER/proxy.conf.$CURRENT_TIME
fi

cp -f proxy.conf $PROXY_CONF

cp $NGINX_CONF $BACKUP_FOLDER/nginx.conf.$CURRENT_TIME
cp -f nginx.conf $NGINX_CONF

if [ -f "$SITE_CONF" ]; then
    cp $SITE_CONF $BACKUP_FOLDER/pingpong-site.$CURRENT_TIME
fi

cp -f pingpong-site $SITE_CONF

if [ -f "$SERVICE_FILE" ]; then
    cp $SERVICE_FILE $BACKUP_FOLDER/kestrel-pingpong.service.$CURRENT_TIME    
fi

cp -f kestrel-pingpong.service $SERVICE_FILE
systemctl enable kestrel-pingpong.service