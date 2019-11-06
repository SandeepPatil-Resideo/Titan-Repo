{{/* vim: set filetype=mustache: */}}
{{/*
Expand the name of the chart.
*/}}
{{- define "titanaddressapi.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "titanaddressapi.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "titanaddressapi.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Return the proper image name
*/}}
{{- define "image" -}}
{{- $registryName := .Values.global.images.imageRegistry -}}
{{- $repositoryName := .Values.global.images.imageRepository -}}
{{- $imageName := .Values.image.name -}}
{{- $tag := default .Values.global.images.imageTag .Values.tagOverride | toString -}}
{{- if .Values.global }}
    {{- if .Values.global.imageRegistry }}
        {{- printf "%s/%s/%s:%s" .Values.global.imageRegistry $repositoryName $imageName $tag -}}
    {{- else -}}
        {{- printf "%s/%s/%s:%s" $registryName $repositoryName $imageName $tag -}}
    {{- end -}}
{{- else -}}
    {{- printf "%s/%s/%s:%s" $registryName $repositoryName $imageName $tag -}}
{{- end -}}
{{- end -}}

{{/*
Return the proper logging image name
*/}}
{{- define "logging.image" -}}
{{- $imageName := .Values.global.images.logging -}}
{{- $tag := .Values.global.images.loggingTag | toString -}}
{{- if .Values.global.images.logging }}
    {{- printf "%s:%s" .Values.global.images.logging $tag -}}
{{- else -}}
    {{- printf "%s:%s" .Values.logging.image.repository $tag -}}
{{- end -}}
{{- end -}}
