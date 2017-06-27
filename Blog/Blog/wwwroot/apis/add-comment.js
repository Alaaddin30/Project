
    $(document).ready(function () {
        var $notify = $("#notify");
        var $btnSubmit = $("#commentSubmit");
        var $isAuth = $("#commentSubmit").attr("data-is-auth");
        var $userName = $("#commentSubmit").attr("data-user-name");
        console.log($isAuth);
        if ($isAuth == "False") {
            document.getElementById("commentSubmit").disabled = true;
        } else {
            $notify.hide();
        }
        $("#commentForm").submit(function (event) {
        event.preventDefault();
        var $form = $(this);
            // BlogId in the  parent view,  ViewBlog => a => #like=> data-blog-id
            $.post("/api/CommentApi", {blogId: $("#like").attr("data-blog-id"), body: $form.find("textarea[name='comment']").val() })
                .done(function (data) {
                    console.log(data);
                    $form.find("textarea[name='comment']").val("");
                        document.getElementById("commentSubmit").disabled = true;
                        $("#commentBody").keyup(function (e) {document.getElementById("commentSubmit").disabled = false; });
                        if (data != null) {
                            AddCommentToDocument(data);

                        }
                });
        });

        var AddCommentToDocument = function (comment) {
            $("#commentsList").append("<div class='message'><p class='blog-meta'> Posted by <a href='#'>" + $userName + "</a> on " + moment(moment(comment.createdAt)).format("MMM dddd YY") + "</p>" + comment.body + "</div><hr />");

    }

    });