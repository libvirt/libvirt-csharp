FROM registry.fedoraproject.org/fedora:32

RUN dnf update -y && \
    dnf install -y \
        ca-certificates \
        git \
        glibc-langpack-en \
        libvirt-devel \
        mono-devel \
        monodevelop && \
    dnf autoremove -y && \
    dnf clean all -y

ENV LANG "en_US.UTF-8"
