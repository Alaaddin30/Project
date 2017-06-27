
    $(document).ready(function () {
        $("#info").hide();
        var $like = $("#like");
        var $dislike=$("#dislike")
        var auth = $("#like").attr("data-auth");
        if (auth == "False") {
            $("a.like").off("click");
            $("a.like").hover(function (event) { $("#info").toggle(); });

        }
        if (auth=="True") {
            $("#like").click(function (event) {
                event.preventDefault();
                $.post("/api/LikeApi", { BlogId: $like.attr("data-blog-id"), Liked: true })
                    .done(function (data) {
                        console.log(data);
                        if (data) {
                            // var total = parseInt($("#totalDislike").text(), 10);
                            var total = +($("#totalLike").text());
                            $("#totalLike").text(total + 1);
                        }
                        $like.off("click");
                    })
            });
            $("#dislike").click(function (event) {
                event.preventDefault();
                $.post("/api/LikeApi", { BlogId: $dislike.attr("data-blog-id"), Liked: false })
                    .done(function (data) {
                        if (data) {
                            var total = +($("#totalDislike").text());
                            $("#totalDislike").text(total + 1);
                        }
                        $like.off("click");
                        
                    })
            });

        }
    });
