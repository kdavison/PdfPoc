ARG REPO=mcr.microsoft.com/dotnet/core/sdk
ARG TAG=3.0-alpine3.9

FROM ${REPO}:${TAG}

LABEL maintainer="kyle.davison@pieinsurance.com"
LABEL version="0.0.0"

ARG HOME=/app

ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

RUN echo "@edge http://nl.alpinelinux.org/alpine/edge/main" >> /etc/apk/repositories && \
    echo "@testing http://nl.alpinelinux.org/alpine/edge/testing" >> /etc/apk/repositories && \
    apk update && \
    apk upgrade && \
    mkdir -p "${HOME}"

WORKDIR ${HOME}

COPY ./* ${HOME}/

RUN dotnet build -c Release

EXPOSE 80

ENTRYPOINT ["sh", "-c", "while [ 1 ]; do sleep 0.2; done"]
